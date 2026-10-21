using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Volo.Abp.DependencyInjection;

namespace LINGYUN.Abp.DataProtection;

public class JavaScriptTypeConvert : IJavaScriptTypeConvert, ISingletonDependency
{
    public JavaScriptTypeConvertResult Convert(Type propertyType)
    {
        var (JavaScriptType, AccessOperates) =  InnerConvert(propertyType);

        return new JavaScriptTypeConvertResult(JavaScriptType, AccessOperates);
    }

    protected virtual (string JavaScriptType, DataAccessFilterOperate[] AccessFilterOperates) InnerConvert(Type propertyType)
    {
        var availableComparator = new List<DataAccessFilterOperate>();
        if (propertyType.IsNullableType())
        {
            propertyType = propertyType.GetGenericArguments().First();
        }

        if (typeof(Enum).IsAssignableFrom(propertyType))
        {
            // The enumeration type only supports the following operators
            // Less than, less than or equal to, greater than, greater than or equal to, equal to, not equal to, empty, not empty
            availableComparator.AddRange(new[]
            {
                DataAccessFilterOperate.Greater,
                DataAccessFilterOperate.GreaterOrEqual,
                DataAccessFilterOperate.Less,
                DataAccessFilterOperate.LessOrEqual,
                DataAccessFilterOperate.Equal,
                DataAccessFilterOperate.NotEqual,
            });
            return ("number", availableComparator.ToArray());
        }

        var typeFullName = propertyType.FullName;

        switch (typeFullName)
        {
            case "System.Int16":
            case "System.Int32":
            case "System.Int64":
            case "System.UInt16":
            case "System.UInt32":
            case "System.UInt64":
            case "System.Single":
            case "System.Double":
            case "System.Byte":
            case "System.SByte":
            case "System.Decimal":
                // Numeric types only support the following operators
                // Less than, less than or equal to, greater than, greater than or equal to, equal to, not equal to, empty, not empty
                availableComparator.AddRange(new[]
                {
                    DataAccessFilterOperate.Greater,
                    DataAccessFilterOperate.GreaterOrEqual,
                    DataAccessFilterOperate.Less,
                    DataAccessFilterOperate.LessOrEqual,
                    DataAccessFilterOperate.Equal,
                    DataAccessFilterOperate.NotEqual,
                });
                return ("number", availableComparator.ToArray());
            case "System.Boolean":
                // The Boolean type only supports the following operators
                // Equal, not equal, empty, not empty
                availableComparator.AddRange(new[]
                {
                    DataAccessFilterOperate.Equal,
                    DataAccessFilterOperate.NotEqual,
                });
                return ("boolean", availableComparator.ToArray());
            case "System.Guid":
                // The Guid type only supports the following operators
                // Equal, not equal, empty, not empty
                availableComparator.AddRange(new[]
                {
                    DataAccessFilterOperate.Equal,
                    DataAccessFilterOperate.NotEqual,
                });
                return ("string", availableComparator.ToArray());
            case "System.Char":
            case "System.String":
                // Character type supports all operators
                return ("string", availableComparator.ToArray());
            case "System.DateTime":
                // The time type only supports the following operators
                // Less than, less than or equal to, greater than, greater than or equal to, equal to, not equal to, empty, not empty
                availableComparator.AddRange(new[]
                {
                    DataAccessFilterOperate.Greater,
                    DataAccessFilterOperate.GreaterOrEqual,
                    DataAccessFilterOperate.Less,
                    DataAccessFilterOperate.LessOrEqual,
                    DataAccessFilterOperate.Equal,
                    DataAccessFilterOperate.NotEqual,
                });
                return ("Date", availableComparator.ToArray());
            default:
            case "System.Object":
            case "System.DBNull":
                if (propertyType.IsArray)
                {
                    // Array type only supports the following operators
                    // Contains, does not contain, empty, not empty
                    availableComparator.AddRange(new[]
                    {
                        DataAccessFilterOperate.Contains,
                        DataAccessFilterOperate.NotContains,
                    });

                    return ("array", availableComparator.ToArray());
                }
                else
                {
                    // Unknown object types only support the following operators
                    // Equal, not equal, empty, not empty
                    availableComparator.AddRange(new[]
                   {
                        DataAccessFilterOperate.Equal,
                        DataAccessFilterOperate.NotEqual,
                    });
                    return ("object", availableComparator.ToArray());
                }
        }
    }
}

readonly workdir=$(cd $(dirname $0); pwd)
echo "Current working directory: $workdir"

echo "Build identity authentication service"
cd $workdir"/../aspnet-core/services/account/AuthServer.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/identityserver --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/identityserver/Dockerfile


echo "Start building identity authentication management service"
cd $workdir"/../aspnet-core/services/identity-server/LINGYUN.Abp.IdentityServer4.HttpApi.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/identityserver4-admin --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/identityserver4-admin/Dockerfile

echo "Start building the background management service"

cd $workdir"/../aspnet-core/services/admin/LINGYUN.Abp.BackendAdmin.HttpApi.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/admin --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/admin/Dockerfile


echo "Start building platform management services"

cd $workdir"/../aspnet-core/services/platform/LINGYUN.Platform.HttpApi.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/platform --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/platform/Dockerfile

echo "Start building message management service"
cd $workdir"/../aspnet-core/services/messages/LINGYUN.Abp.MessageService.HttpApi.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/messages --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/messages/Dockerfile

echo "Start building localized management services"
cd $workdir"/../aspnet-core/services/localization/LINGYUN.Abp.LocalizationManagement.HttpApi.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/localization --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/localization/Dockerfile

echo "Start building gateway management service"
cd $workdir"/../aspnet-core/services/apigateway/LINGYUN.ApiGateway.HttpApi.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/apigateway-admin --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/apigateway-admin/Dockerfile

echo "Start building gateway service"
cd $workdir"/../aspnet-core/services/apigateway/LINGYUN.ApiGateway.Host"

dotnet restore
dotnet publish -c Release -o ../../Publish/apigateway-host --no-cache --no-restore
cp -r -f Dockerfile ../../Publish/apigateway-host/Dockerfile

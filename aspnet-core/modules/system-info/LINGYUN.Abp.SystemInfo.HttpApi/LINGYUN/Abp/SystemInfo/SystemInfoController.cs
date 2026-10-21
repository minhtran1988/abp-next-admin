using DotNetCore.CAP;
using LINGYUN.Abp.SystemInfo.Models;
using LINGYUN.Abp.SystemInfo.Permissions;
using LINGYUN.Abp.SystemInfo.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace LINGYUN.Abp.SystemInfo;

[Controller]
[Route("api/system-info")]
[Authorize(SystemInfoPermissions.Default)]
public class SystemInfoController : AbpControllerBase
{
    [HttpGet]
    public async virtual Task<SystemInfoModel> GetSystemInfoAsync()
    {
        return new SystemInfoModel
        {
            Components = new ComponentInfoModel[]
            {
                GetSystemInfo(),
                GetPerformanceInfo(),
                GetRedisInfo(),
                GetCapInfo(),
            },
        };
    }

    private ComponentInfoModel GetSystemInfo()
    {
        var env = HttpContext.RequestServices.GetService<IHostEnvironment>();
        using var process = Process.GetCurrentProcess();

        var systemKeys = new List<ComponentKeyModel>
        {
            new ComponentKeyModel("sys_machine_name", "machine name"),
            new ComponentKeyModel("sys_environment", "Runtime environment"),
            new ComponentKeyModel("sys_framework_version", ".NET version"),
            new ComponentKeyModel("sys_os_version", "operating system version"),
            new ComponentKeyModel("sys_app_db_provider", "database"),
            new ComponentKeyModel("sys_app_version", "Application version"),
            new ComponentKeyModel("sys_app_build_time", "Compilation time"),
            new ComponentKeyModel("sys_app_start_time", "Start time"),
        };

        var systemDetails = new Dictionary<string, object?>
        {
            { "sys_machine_name", Environment.MachineName },
            { "sys_environment", env?.EnvironmentName ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") },
            { "sys_framework_version", RuntimeInformation.FrameworkDescription },
            { "sys_os_version", RuntimeInformation.OSDescription },
            { "sys_app_db_provider", Environment.GetEnvironmentVariable("APPLICATION_DATABASE_PROVIDER") },
            { "sys_app_start_time", process.StartTime.ToString("yyyy-MM-dd HH:mm:ss") },
        };

        try
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var assemblyName = assembly.GetName();
            var buildTime = GetBuildTime(assembly);

            systemDetails.Add("sys_app_version", fileVersionInfo.FileVersion ?? assemblyName.Version?.ToString() ?? "N/A");
            systemDetails.Add("sys_app_build_time", buildTime.HasValue ? buildTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "N/A");
        }
        catch
        {
            systemDetails.Add("sys_app_version", "N/A");
            systemDetails.Add("sys_app_build_time", "N/A");
        }

        return new ComponentInfoModel("system", systemKeys.ToArray(), systemDetails);
    }

    private ComponentInfoModel GetPerformanceInfo()
    {
        using var process = Process.GetCurrentProcess();

        var memoryMetrics = new MemoryMonitor().GetMemoryMetrics();
        var threadMetrics = new ThreadMonitor().GetThreadMetrics();
        var gcMetrics = new GCMonitor().GetGCMetrics();

        return new ComponentInfoModel(
            "Performance",
            new ComponentKeyModel[]
            {
                new ComponentKeyModel("perf_total_memory", "Total memory (MB)"),
                new ComponentKeyModel("perf_working_set", "Working set memory (MB)"),
                new ComponentKeyModel("perf_private_memory", "Private memory (MB)"),
                new ComponentKeyModel("perf_gc_heap_size", "GC heap size (MB)"),
                new ComponentKeyModel("perf_gc0", "Gen0 recycling times"),
                new ComponentKeyModel("perf_gc1", "Gen1 recycling times"),
                new ComponentKeyModel("perf_gc2", "Gen2 recycling times"),
                new ComponentKeyModel("perf_active_total_memory", "Total available memory"),
                new ComponentKeyModel("perf_memory_load", "memory load"),
                new ComponentKeyModel("perf_fragmented_bytes", "Memory fragment size"),
                new ComponentKeyModel("perf_available_worker_threads", "Number of available worker threads"),
                new ComponentKeyModel("perf_available_completion_port_threads", "Number of available I/O threads"),
                new ComponentKeyModel("perf_max_worker_threads", "Maximum number of worker threads"),
                new ComponentKeyModel("perf_max_completion_port_threads", "Maximum number of I/O threads"),
                new ComponentKeyModel("perf_active_threads", "Total number of threads in the process"),
                new ComponentKeyModel("perf_thread_pool_thread_count", "Number of active threads in the thread pool"),
            },
            new Dictionary<string, object?>
            {
                { "perf_total_memory", memoryMetrics.TotalMemory },
                { "perf_working_set", memoryMetrics.WorkingSet },
                { "perf_private_memory", memoryMetrics.PrivateMemory },
                { "perf_gc_heap_size", memoryMetrics.GCHeapSize },
                { "perf_gc0", gcMetrics.Gen0Collections },
                { "perf_gc1", gcMetrics.Gen1Collections },
                { "perf_gc2", gcMetrics.Gen2Collections },
                { "perf_active_total_memory", gcMetrics.TotalMemory },
                { "perf_memory_load", gcMetrics.MemoryLoad },
                { "perf_fragmented_bytes", gcMetrics.FragmentedBytes },
                { "perf_available_worker_threads", threadMetrics.AvailableWorkerThreads },
                { "perf_available_completion_port_threads", threadMetrics.AvailableCompletionPortThreads },
                { "perf_max_worker_threads", threadMetrics.MaxWorkerThreads },
                { "perf_max_completion_port_threads", threadMetrics.MaxCompletionPortThreads },
                { "perf_active_threads", threadMetrics.ActiveThreads },
                { "perf_thread_pool_thread_count", threadMetrics.ThreadPoolThreadCount },
            });
    }

    private ComponentInfoModel GetCapInfo()
    {
        var cap = HttpContext.RequestServices.GetService<CapMarkerService>();
        var storage = HttpContext.RequestServices.GetService<CapStorageMarkerService>();
        var broker = HttpContext.RequestServices.GetService<CapMessageQueueMakerService>();

        var capKeys = new List<ComponentKeyModel>
        {
            new ComponentKeyModel("cap_status", "status"),
            new ComponentKeyModel("cap_version", "version"),
            new ComponentKeyModel("cap_storage", "persistence"),
            new ComponentKeyModel("cap_transport", "transport"),
        };
        var capDetails = new Dictionary<string, object?>
        {
            { "cap_status", cap != null ? "normal" : "not enabled" },
            { "cap_version", cap != null ? cap.Version.Substring(0, 5) : "N/A" },
            { "cap_storage", storage != null ? storage.Name : "N/A" },
            { "cap_transport", broker != null ? broker.Name : "N/A" }
        };

        return new ComponentInfoModel("CAP component", capKeys.ToArray(), capDetails);
    }

    private ComponentInfoModel GetRedisInfo()
    {
        var redis = HttpContext.RequestServices.GetService<IConnectionMultiplexer>();
        if (redis == null)
        {
            return new ComponentInfoModel(
                "Redis component",
                new ComponentKeyModel[]
                {
                    new ComponentKeyModel("redis_status", "status")
                },
                new Dictionary<string, object?>
                {
                    { "redis_status", "Not registered" },
                });
        }
        var redisInfo = new Dictionary<string, string>();

        try
        {
            var server = redis.GetServer(redis.GetEndPoints().First());
            var serverInfo = server.Info();
            foreach (var section in serverInfo)
            {
                foreach (var item in section)
                {
                    redisInfo[item.Key] = item.Value;
                }
            }
        }
        catch(Exception ex)
        {
            Logger.LogWarning("There is an exception in connecting to the redis server: {message}", ex.Message);

            return new ComponentInfoModel(
                "Redis component",
                new ComponentKeyModel[]
                {
                    new ComponentKeyModel("redis_status", "status")
                },
                new Dictionary<string, object?>
                {
                    { "redis_status", "Connection exception" },
                });
        }

        var uptimeSeconds = redisInfo.ContainsKey("uptime_in_seconds") ? long.Parse(redisInfo["uptime_in_seconds"]) : 0;
        var days = Math.Floor((double)uptimeSeconds / 86400);
        var hours = Math.Floor((double)uptimeSeconds % 86400 / 3600);
        var minutes = Math.Floor((double)uptimeSeconds % 3600 / 60);

        var usedMemory = redisInfo.ContainsKey("used_memory") ? long.Parse(redisInfo["used_memory"]) : 0;
        var maxMemory = redisInfo.ContainsKey("maxmemory") ? long.Parse(redisInfo["maxmemory"]) : 0;
        var totalKeys = CalculateTotalKeys(redisInfo);
        var expiresKeys = CalculateExpires(redisInfo);
        var avgTtl = CalculateAvgTtl(redisInfo);

        var redisKeys = new List<ComponentKeyModel>()
        {
            new ComponentKeyModel("redis_version", "version"),
            new ComponentKeyModel("redis_mode", "Run mode"),
            new ComponentKeyModel("redis_os", "operating system"),
            new ComponentKeyModel("redis_uptime", "Running time"),
            new ComponentKeyModel("redis_used_cpu_sys", "System CPU usage time"),
            new ComponentKeyModel("redis_used_cpu_user", "User CPU usage time"),
            new ComponentKeyModel("redis_used_memory_mb", "Memory used (MB)"),
            new ComponentKeyModel("redis_used_memory_rss_mb", "Physical Memory (MB)"),
            new ComponentKeyModel("redis_used_memory_peak_mb", "Peak memory usage (MB)"),
            new ComponentKeyModel("redis_maxmemory_mb", "Maximum memory limit (MB)"),
            new ComponentKeyModel("redis_memory_usage_percent", "Memory usage (%)"),
            new ComponentKeyModel("redis_mem_fragmentation_ratio", "memory fragmentation rate"),
            new ComponentKeyModel("redis_connected_clients", "Number of connected clients"),
            new ComponentKeyModel("redis_blocked_clients", "Number of blocked clients"),
            new ComponentKeyModel("redis_client_longest_output_list", "Client's longest output list"),
            new ComponentKeyModel("redis_client_biggest_input_buf", "Client maximum input buffer"),
            new ComponentKeyModel("redis_maxclients", "Maximum number of clients"),
            new ComponentKeyModel("redis_total_keys", "Total number of keys"),
            new ComponentKeyModel("redis_expires_keys", "Set the number of expired keys"),
            new ComponentKeyModel("redis_expired_keys", "Number of expired keys"),
            new ComponentKeyModel("redis_evicted_keys", "Number of evicted keys"),
            new ComponentKeyModel("redis_avg_ttl_seconds", "Average TTL (seconds)"),
        };
        var redisDetails = new Dictionary<string, object?>()
        {
            { "redis_version", redisInfo.GetValueOrDefault("redis_version", "unknown") },
            { "redis_mode", redisInfo.GetValueOrDefault("redis_mode", "unknown") },
            { "redis_os", redisInfo.GetValueOrDefault("os", "unknown") },
            { "redis_uptime", $"{days}days{hours}hours{minutes}minutes" },
            { "redis_used_cpu_sys", redisInfo.GetValueOrDefault("used_cpu_sys", "0") },
            { "redis_used_cpu_user", redisInfo.GetValueOrDefault("used_cpu_user", "0") },
            { "redis_used_memory_mb", Math.Round((double)usedMemory / 1024 / 1024, 2) },
            { "redis_used_memory_rss_mb", redisInfo.ContainsKey("used_memory_rss") ?
                    Math.Round(double.Parse(redisInfo["used_memory_rss"]) / 1024 / 1024, 2) : 0 },
            { "redis_used_memory_peak_mb", redisInfo.ContainsKey("used_memory_peak") ?
                    Math.Round(double.Parse(redisInfo["used_memory_peak"]) / 1024 / 1024, 2) : 0 },
            { "redis_maxmemory_mb", maxMemory > 0 ? Math.Round((double)maxMemory / 1024 / 1024, 2) : 0 },
            { "redis_memory_usage_percent", maxMemory > 0 ? Math.Round((double)usedMemory / maxMemory * 100, 2) : 0 },
            { "redis_mem_fragmentation_ratio", redisInfo.GetValueOrDefault("mem_fragmentation_ratio", "0") },
            { "redis_connected_clients", redisInfo.GetValueOrDefault("connected_clients", "0") },
            { "redis_blocked_clients", redisInfo.GetValueOrDefault("blocked_clients", "0") },
            { "redis_client_longest_output_list", redisInfo.GetValueOrDefault("client_longest_output_list", "0") },
            { "redis_client_biggest_input_buf", redisInfo.GetValueOrDefault("client_biggest_input_buf", "0") },
            { "redis_maxclients", redisInfo.GetValueOrDefault("maxclients", "unlimited") },
            { "redis_total_keys", totalKeys },
            { "redis_expires_keys", expiresKeys },
            { "redis_expired_keys", redisInfo.GetValueOrDefault("expired_keys", "0") },
            { "redis_evicted_keys", redisInfo.GetValueOrDefault("evicted_keys", "0") },
            { "redis_avg_ttl_seconds", avgTtl },
        };

        return new ComponentInfoModel("Redis component", redisKeys.ToArray(), redisDetails);
    }

    private long CalculateTotalKeys(Dictionary<string, string> redisInfo)
    {
        long total = 0;

        foreach (var item in redisInfo)
        {
            if (item.Key.StartsWith("db"))
            {
                var keysPart = item.Value.Split(',').FirstOrDefault(x => x.StartsWith("keys="));
                if (keysPart != null && long.TryParse(keysPart.Split('=')[1], out var count))
                {
                    total += count;
                }
            }
        }

        return total;
    }

    private long CalculateExpires(Dictionary<string, string> redisInfo)
    {
        long expires = 0;

        foreach (var item in redisInfo)
        {
            if (item.Key.StartsWith("db"))
            {
                var expiresPart = item.Value.Split(',').FirstOrDefault(x => x.StartsWith("expires="));
                if (expiresPart != null && long.TryParse(expiresPart.Split('=')[1], out var count))
                {
                    expires += count;
                }
            }
        }

        return expires;
    }

    private long CalculateAvgTtl(Dictionary<string, string> redisInfo)
    {
        long totalTtl = 0;
        long dbCount = 0;

        foreach (var item in redisInfo)
        {
            if (item.Key.StartsWith("db"))
            {
                var ttlPart = item.Value.Split(',').FirstOrDefault(x => x.StartsWith("avg_ttl="));
                if (ttlPart != null && long.TryParse(ttlPart.Split('=')[1], out var ttl) && ttl > 0)
                {
                    totalTtl += ttl;
                    dbCount++;
                }
            }
        }

        return dbCount > 0 ? totalTtl / dbCount : -1;
    }

    private DateTime? GetBuildTime(Assembly assembly)
    {
        try
        {
            var attribute = assembly.GetCustomAttribute<AssemblyMetadataAttribute>();
            if (attribute != null && attribute.Key == "BuildTimestamp")
            {
                if (DateTime.TryParse(attribute.Value, out var buildTime))
                {
                    return buildTime;
                }
            }

            var fileInfo = new FileInfo(assembly.Location);
            if (fileInfo.Exists)
            {
                return fileInfo.LastWriteTime;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}

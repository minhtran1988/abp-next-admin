using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace LINGYUN.Abp.Identity.Session;
/// <summary>
/// User session persistence
/// </summary>
public interface IIdentitySessionStore
{
    /// <summary>
    /// Create user session
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <param name="device">Device</param>
    /// <param name="deviceInfo">Device information</param>
    /// <param name="userId">user id</param>
    /// <param name="clientId">Client id</param>
    /// <param name="ipAddresses">ip addresses</param>
    /// <param name="signedIn">Login time</param>
    /// <param name="lastAccessed">Last access time</param>
    /// <param name="ipRegion">IP region</param>
    /// <param name="userName">User name</param>
    /// <param name="tenantId">Tenant id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created <seealso cref="IdentitySession"/></returns>
    Task<IdentitySession> CreateAsync(
        string sessionId,
        string device,
        string? deviceInfo,
        Guid userId,
        string? clientId,
        string? ipAddresses,
        DateTime signedIn,
        DateTime? lastAccessed = null,
        string? ipRegion = null,
        string? userName = null,
        Guid? tenantId = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Update user session
    /// </summary>
    /// <param name="session">User session entity</param>
    /// <param name="cancellationToken"></param>
    /// <returns>a completed task.</returns>
    Task UpdateAsync(
        IdentitySession session,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query user session
    /// </summary>
    /// <param name="id">session key</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The queried <seealso cref="IdentitySession"/></returns>
    /// <exception cref="Volo.Abp.Domain.Entities.EntityNotFoundException"></exception>
    Task<IdentitySession> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query user session
    /// </summary>
    /// <param name="id">session key</param>
    /// <param name="cancellationToken"></param>
    /// <returns>If an instance of <seealso cref="IdentitySession"/> exists, return null.</returns>
    Task<IdentitySession?> FindAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query user session
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The queried <seealso cref="IdentitySession"/></returns>
    /// <exception cref="Volo.Abp.Domain.Entities.EntityNotFoundException"></exception>
    Task<IdentitySession> GetAsync(
        string sessionId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query user session
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>If an instance of <seealso cref="IdentitySession"/> exists, return null.</returns>
    Task<IdentitySession?> FindAsync(
        string sessionId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query the user's last session
    /// </summary>
    /// <param name="userId">user id</param>
    /// <param name="device">Device</param>
    /// <param name="cancellationToken"></param>
    /// <returns>If an instance of <seealso cref="IdentitySession"/> exists, return null.</returns>
    Task<IdentitySession?> FindLastAsync(
        Guid userId,
        string device,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Whether the session already exists
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>If it exists, return true, otherwise return false.</returns>
    Task<bool> ExistAsync(
        string sessionId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel user session
    /// </summary>
    /// <param name="id">session key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel user session
    /// </summary>
    /// <param name="sessionId">session id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeAsync(
        string sessionId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel all sessions
    /// </summary>
    /// <param name="userId">user id</param>
    /// <param name="exceptSessionId">Exclude session key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeAllAsync(
        Guid userId,
        Guid? exceptSessionId = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel all sessions
    /// </summary>
    /// <param name="userId">user id</param>
    /// <param name="device">Device</param>
    /// <param name="exceptSessionId">Exclude session key</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeAllAsync(
        Guid userId,
        string device,
        Guid? exceptSessionId = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel inactive session
    /// </summary>
    /// <param name="inactiveTimeSpan">Inactive time interval</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeAllAsync(
        TimeSpan inactiveTimeSpan,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel the specified session
    /// </summary>
    /// <param name="userId">user id</param>
    /// <param name="device">Device</param>
    /// <param name="exceptSessionId">Exclude session id</param>
    /// <param name="maxCount">Maximum quantity</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeWithAsync(
        Guid userId,
        string? device = null,
        Guid? exceptSessionId = null,
        int maxCount = 0,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel session list
    /// </summary>
    /// <param name="identitySessions">session list</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RevokeManyAsync(
        IEnumerable<IdentitySession> identitySessions,
        CancellationToken cancellationToken = default);
}

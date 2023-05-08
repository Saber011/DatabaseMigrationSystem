using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using DatabaseMigrationSystem.Common.Dto;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Account.Commands;

/// <summary>
/// 
/// </summary>
[DataContract]
public class AuthenticateCommand : IRequest<AuthenticateInfo>
{
    /// <summary>
    /// 
    /// </summary>
    [DataMember]
    public string Login { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [DataMember]
    public string Password { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string IpAddress { get; set; }
}
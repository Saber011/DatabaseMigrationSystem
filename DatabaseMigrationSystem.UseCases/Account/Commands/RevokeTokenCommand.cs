using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MediatR;

namespace DatabaseMigrationSystem.UseCases.Account.Commands;

[DataContract]
public class RevokeTokenCommand : IRequest
{
    /// <summary>
    /// 
    /// </summary>
    [DataMember]
    public string Token { get; set; }
    
    
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string IpAddess { get; set; }
}
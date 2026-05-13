namespace Database;

public enum DynamicsAuthenticationType
{
    OnPremise,
    Cloud,
}

public class DynamicsTokenProviderOptions
{
    public DynamicsAuthenticationType AuthenticationType { get; set; }
    public string DynamicsApiEndpointUrl { get; set; }
    public ADFSTokenProviderOptions ADFS { get; set; }
    public EntraIdTokenProviderOptions EntraId { get; set; }
}

public class ADFSTokenProviderOptions
{
    public string OAuth2TokenEndpoint { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ServiceAccountDomain { get; set; }
    public string ServiceAccountName { get; set; }
    public string ServiceAccountPassword { get; set; }
    public string ResourceName { get; set; }
}

public class EntraIdTokenProviderOptions
{
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ResourceName { get; set; }
}

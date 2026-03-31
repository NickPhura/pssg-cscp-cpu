namespace Gov.Cscp.Victims.Public.Models
{
    /// <summary>
    /// Response returned after submitting org or staff updates via vsd_SetCPUOrgContracts.
    /// </summary>
    public class SetOrgContractsResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Result { get; set; }
    }
}

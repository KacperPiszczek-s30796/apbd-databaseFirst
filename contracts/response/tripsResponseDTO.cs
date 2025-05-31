namespace DatabaseFirstAproach.contracts.response;

public class tripsResponseDTO
{
    public int pageNum{ get; set; } = 1;
    public int pageSize{ get; set; } = 10;
    public int allPages{ get; set; }
    public List<tripDTO> trips { get; set; }
}
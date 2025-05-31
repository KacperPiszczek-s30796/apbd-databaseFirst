namespace DatabaseFirstAproach.contracts.response;

public class tripDTO
{
    private string Name{ get; set; }
    private string Description{ get; set; }
    private DateTime DateFrom{ get; set; }
    private DateTime DateTo{ get; set; }
    private int MaxPeople{ get; set; }
    private List<CountryDTO> Countries { get; set; }
    private List<ClientDTO> Clients{ get; set; }
}
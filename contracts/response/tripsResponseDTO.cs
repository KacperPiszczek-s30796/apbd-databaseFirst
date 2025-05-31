namespace DatabaseFirstAproach.contracts.response;

public class tripsResponseDTO
{
    private int pageNum = 1;
    private int pageSize = 10;
    private int allPages{ get; set; }
    private List<tripDTO> trips { get; set; }

    public int Get_pageNum()
    {
        return pageNum;
    }

    public void Set_pageNum(int pageNum)
    {
        this.pageNum = pageNum;
    }

    public int Get_pageSize()
    {
        return pageSize;
    }

    public void Set_pageSize(int pageSize)
    {
        this.pageSize = pageSize;
    }
}
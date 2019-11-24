namespace BusinessLayer
{
    public interface IBusinessAccessService
    {
        GetObjects Get { get; set; }
        SaveObjects Save { get; set; }
    }
}
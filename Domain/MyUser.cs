namespace AspnetOkta.Domain
{
  public class MyUser{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FavoriteColor { get; set; }
    public CustomData CustomData { get; set; }
  }
}
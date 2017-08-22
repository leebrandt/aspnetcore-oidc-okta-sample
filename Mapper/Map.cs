using AspnetOkta.Domain;
using Okta.Sdk;

namespace AspnetOkta.Mapper
{
  public static class Map
  {
    public static MyUser ToMyUser(IUser user)
    {
      return new MyUser{
        Id = user.Id,
        FirstName = user.Profile["firstName"].ToString(),
        LastName = user.Profile["lastName"].ToString(),
        FavoriteColor = user.Profile["favoriteColor"].ToString()
      };
    }
  }
}
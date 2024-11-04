namespace chd.CaraVan.Contracts.Settings
{
    public class EmailSettings
  {
      public string Smtp { get; set; }
      public int Port{ get; set; }
      public string Username{ get; set; }
      public string Password{ get; set; }
      public string From{ get; set; }
      public string FromName{ get; set; }
      public string To{ get; set; }
  }
}

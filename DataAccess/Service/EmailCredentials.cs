namespace DataAccess.Service
{
    public class EmailCredentials
    {
        public string username { get; set; }
        public string password { get; set; }
        public int port { get; set; }
        public string client {  get; set; }  
        public bool ssl { get; set; } 
    }
}
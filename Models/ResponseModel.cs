namespace UserManagement_Serv.Models
{
    public class ResponseModel
    {
        public String Message { get; set; }
        public String Token { get; set; }
        public String Name { get; set; }

        public ResponseModel(string message, string token, string name)
        {
            Message = message;
            Token = token;
            Name = name;    
        }
    }
}

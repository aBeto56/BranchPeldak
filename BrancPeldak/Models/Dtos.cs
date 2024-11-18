namespace BrancPeldak.Models
{
    
        public record CreatePlayerDto(string Id, string Name, int Height, int Weight);

        public record UpdatePlayerDto(string Id, string Name, int Height, int Weight);

        public record CreateDatDto(string Id, DateTime Subbed_In, int Try, int Goal, int Fault, string PlayerId);

        public record UpdateDatDto(DateTime Subb_Out, int Try, int Goal, int Fault);

}


using tododotnet6prac.Models;

namespace tododotnet6prac.Dtos
{
    public class TodoListSelectDto
    {
        public Guid TodoId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime InsertTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public bool Enable { get; set; }

        public int Orders { get; set; }


        public  string InsertEmployee { get; set; } = null!;

        public  string UpdateEmployee { get; set; } = null!;
    }
}

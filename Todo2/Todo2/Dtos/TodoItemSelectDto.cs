namespace Todo2.Dtos
{
    public class TodoItemSelectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsComplete { get; set; }

        public string NameId { get; set; } = null!;
    }
}

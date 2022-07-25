namespace ToDoAPI.Entities
{
    public class ToDoEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool Done { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

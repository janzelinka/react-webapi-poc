namespace app_api.Models.Interfaces {    
    public interface ICrudService<T> {
        public Guid Create(T item);
        public IEnumerable<T> GetAll();
        public Guid Update(T item);
        public void Delete(T item);
    }
}
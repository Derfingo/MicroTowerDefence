namespace MicroTowerDefence
{
    public interface IViewModel<T>
    {
        ReactiveProperty<T> Property { get; }
    }
}

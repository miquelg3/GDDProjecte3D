public class Material : Item
{
    public int Cantidad { get; private set; }
    public Material(string id, string nombre) : base(id, nombre)
    {
    }
}
public enum TipoMateriales
{

}

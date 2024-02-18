public class Material : Item
{
    public int Cantidad { get; private set; }
    public TipoMaterial TipoMaterial { get; private set; }
    public Material(string id, string nombre, bool acumulable, TipoMaterial tipoMaterial) : base(id, nombre, acumulable)
    {
        TipoMaterial = tipoMaterial;
    }
}
public enum TipoMaterial
{
    Quirurgico,
    Hierba,
    Polvo
}

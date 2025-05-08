
public class TotalStats {
    static TotalStats instance;
    public static TotalStats Instance
    {
        get
        {
            instance ??= new TotalStats();
            return instance;
        }
    }
    public int kill;
    public float time;
    public int coin;
}

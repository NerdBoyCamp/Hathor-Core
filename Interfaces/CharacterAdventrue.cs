namespace Hathor
{
    // 冒险相关
    public interface ICharacterAdventure
    {
        // 感应
        int Perception { get; set; }

        // 运气
        int Luck { get; set; }

        // 口才
        int Eloquence { get; set; }

        // 相貌
        int Appearance { get; set; }
    }
}

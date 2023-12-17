using TodoList.Utils.Enums;

namespace TodoList.Utils
{
    public class Constants
    {
        public static string DATA_PATH = "/Resources/Data/tasks.json";
        public static Dictionary<ItemCategory, string> CATEGORY_COLORS = new Dictionary<ItemCategory, string>()
        {
            { ItemCategory.Travail, "#0046FF" },
            { ItemCategory.Loisirs, "#27ae60" },
            { ItemCategory.Santé, "#e74c3c" },
            { ItemCategory.Alimentation, "#8e44ad" },
            { ItemCategory.Ménage, "#f39c12" },
            { ItemCategory.Autre, "#000000" },
        };
    }
}

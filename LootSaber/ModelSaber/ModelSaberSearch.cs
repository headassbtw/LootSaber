//https://github.com/legoandmars/ModelDownloader/blob/master/ModelDownloader/Types/ModelsaberSearch.cs

namespace LootSaber.ModelSaber
{
    public class ModelSaberSearch
    {
        public ModelSaberSearchType ModelType = ModelSaberSearchType.All;
        public ModelSaberSearchSort ModelSort = ModelSaberSearchSort.Newest;
        public string Search = "";
        public int Page = 0;

        public readonly int PageLength = 18;

        public ModelSaberSearch(ModelSaberSearchType type = ModelSaberSearchType.All, int page = 0, ModelSaberSearchSort sort = ModelSaberSearchSort.Newest, string search = "")
        {
            ModelType = type;
            Page = page;
            ModelSort = sort;
            Search = search;
        }
    }

    public enum ModelSaberSearchType
    {
        All = 0,
        Saber = 1,
        Bloq = 2,
        Platform = 3,
        Avatar = 4
    }
    public enum ModelSaberSearchSort
    {
        Newest = 0,
        Oldest = 1,
        Name = 2,
        Author = 3,
    }
}
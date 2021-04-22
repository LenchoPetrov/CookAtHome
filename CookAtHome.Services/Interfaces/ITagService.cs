using CookAtHome.Data.Models;
using CookAtHome.Services.Models;
using System.Collections.Generic;

namespace CookAtHome.Services.Interfaces
{
    public interface ITagService
    {
        HashSet<Tag> CheckTags(HashSet<string> tags);

        HashSet<TagInfo> GetAll();

        HashSet<TagInfo> GetSearchedTags(string text);
    }
}

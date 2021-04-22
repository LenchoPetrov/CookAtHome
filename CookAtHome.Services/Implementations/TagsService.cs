using CookAtHome.Data.Data;
using CookAtHome.Data.Models;
using CookAtHome.Services.Interfaces;
using CookAtHome.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookAtHome.Services.Implementations
{
    public class TagsService : ITagService
    {
        private readonly CookAtHomeDbContext db;

        public TagsService(CookAtHomeDbContext db)
        {
            this.db = db;
        }

        public HashSet<Tag> CheckTags(HashSet<string> tags)
        {
            var tagsList = new HashSet<Tag>();
            foreach (var tag in tags)
            {
                if (tag != null)
                {
                    var tagExist = this.db.Tags.Any(u => u.Title == tag);
                    var tagToAdd = new Tag();
                    if (!tagExist)
                    {
                        tagToAdd = new Tag
                        {
                            Title = tag
                        };
                        this.db.Add(tagToAdd);
                        tagsList.Add(tagToAdd);
                    }
                    else
                    {
                        tagToAdd = this.db.Tags.FirstOrDefault(t => t.Title == tag);
                        tagsList.Add(tagToAdd);
                    }
                }
            }
            this.db.SaveChanges();
            return tagsList;
        }

        public HashSet<TagInfo> GetAll()
        {
            try
            {
                return this.db.Tags.Select(t => new TagInfo { Id = t.Id, Title = t.Title }).ToHashSet();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public HashSet<TagInfo> GetSearchedTags(string text)
        {
            try
            {
                return this.db.Tags.Where(t=>t.Title.Contains(text)).Select(t => new TagInfo { Id = t.Id, Title = t.Title }).ToHashSet();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

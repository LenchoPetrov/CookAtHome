using CookAtHome.Services.Models;
using System.Collections.Generic;

namespace CookAtHome.Services.Interfaces
{
    public interface ICommentService
    {
        int CreateComment(int recipeId, string commentContent, string id);

        int DeleteComment(int commentId);

        bool CheckCommentDeletePermision(string userId, int commentId);
        
        HashSet<CommentByUser> GetCommentsByUser(string username);
    }
}

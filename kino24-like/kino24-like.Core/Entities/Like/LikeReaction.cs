using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kino24_like.Core.Entities
{
    public enum LikeReaction
    {
        [Description("Like")]
        Like = 1,

        [Description("Dislike")]
        Dislike = -1
    }
}

using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernWarfareSBMM
{
    public class UserModel
    {
        public short Rank { get; private set; }
        public string Username { get; private set; }

        public Block Block { get; private set; }

        /// <summary>
        /// Create a new User.
        /// </summary>
        /// <param name="rank">The user's rank. This will be the first item found for each user (even before username).</param>
        /// <param name="block">The Google Vision Block this was found in.</param>
        public UserModel(short rank, Block block)
        {
            this.Rank = rank;
            this.Block = block;
        }

        public void SetUsername(string username)
        {
            this.Username = username;
        }

        public void AppendToUsername(string piece)
        {
            this.Username += $" {piece}";
        }

        public bool IsComplete()
        {
            return this.Rank > 0 && !string.IsNullOrEmpty(this.Username);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CS478_EventPlannerProject.Services
{
    public class ContentModerationService
    {
        // Prohibited keywords organized by category
        private static readonly Dictionary<string, List<string>> ProhibitedKeywords = new Dictionary<string, List<string>>
        {
            // Sexual content keywords
            ["sexual"] = new List<string>
            {
                "orgy", "orgies", "gloryhole", "glory hole", "nsfw", "xxx",
                "sex party", "adult entertainment", "strip club", "stripper",
                "escort", "hookup", "one night stand", "booty call", "sexting",
                "swingers", "swinger", "adult film", "pornography", "porn", "sex", "dick", "vagina"
            },

            // Dating event keywords (major dating events only)
            ["dating"] = new List<string>
            {
                "speed dating", "singles mixer", "singles night", "singles event",
                "matchmaking event", "matchmaker", "find your match", "dating game",
                "blind date event", "singles only", "looking for love", "meet singles"
            },

            // Drug-related keywords
            ["drugs"] = new List<string>
            {
                "420 party", "420 event", "weed party", "pot party", "marijuana party",
                "cannabis party", "mdma", "ecstasy", "cocaine", "heroin", "meth",
                "drug party", "get high", "trip party", "psychedelic party",
                "molly party", "acid party", "shroom", "mushroom party"
            },

            // Violent/BDSM/Masochist content keywords
            ["violence"] = new List<string>
            {
                "bdsm", "bdsm party", "fetish party", "kink event", "dominatrix",
                "masochist", "sadist", "bondage", "dungeon party", "pain play",
                "whipping", "torture", "extreme pain", "blood sport", "fight club"
            }
        };

        /// <summary>
        /// Checks if event content violates community guidelines
        /// </summary>
        /// <param name="eventName">The event name</param>
        /// <param name="eventDescription">The event description</param>
        /// <param name="eventDetails">Additional event details</param>
        /// <returns>True if content violates guidelines, false otherwise</returns>
        public static bool ViolatesGuidelines(string eventName, string eventDescription = null, string eventDetails = null)
        {
            // Combine all text fields for checking
            var combinedText = $"{eventName} {eventDescription} {eventDetails}".ToLowerInvariant();

            // Check against all prohibited keyword categories
            foreach (var category in ProhibitedKeywords.Values)
            {
                foreach (var keyword in category)
                {
                    if (combinedText.Contains(keyword.ToLowerInvariant()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a list of matched prohibited keywords for detailed feedback
        /// </summary>
        public static List<string> GetViolatingKeywords(string eventName, string eventDescription = null, string eventDetails = null)
        {
            var combinedText = $"{eventName} {eventDescription} {eventDetails}".ToLowerInvariant();
            var matchedKeywords = new List<string>();

            foreach (var category in ProhibitedKeywords.Values)
            {
                foreach (var keyword in category)
                {
                    if (combinedText.Contains(keyword.ToLowerInvariant()))
                    {
                        matchedKeywords.Add(keyword);
                    }
                }
            }

            return matchedKeywords;
        }

        /// <summary>
        /// Gets a user-friendly error message for content violations
        /// </summary>
        public static string GetViolationMessage()
        {
            return "Your event contains content that violates our community guidelines. " +
                   "We maintain a family-friendly platform and do not allow events involving: " +
                   "sexual content, major dating events, drug use, or violent/masochist activities. " +
                   "Please review your event details and try again.";
        }
    }
}
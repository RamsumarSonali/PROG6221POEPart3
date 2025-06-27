using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotLibrary.Core
{
    
    
        public class ActivityLog
        {
            private List<string> entries = new List<string>();

            public void AddEntry(string entry)
            {
                entries.Add($"{DateTime.Now}: {entry}");
            }

            public string GetRecentEntries()
            {
                int count = entries.Count >= 5 ? 5 : entries.Count;
                var recent = entries.GetRange(entries.Count - count, count);
                return string.Join(Environment.NewLine, recent);
            }
        }
    }


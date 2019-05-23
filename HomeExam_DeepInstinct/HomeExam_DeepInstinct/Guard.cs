using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeExam_DeepInstinct
{

    public class Guard
    {
        public string ID { get; set; }

        public int TotalSleep { get; set; }

        private Dictionary<string, int> m_MostLikelyOfSleep = new Dictionary<string, int>();

        public string MaxMinute { get; set; }

        public int ValueOfMaxMinute { get; set; }

        public Dictionary<string, int> MostLikelyOfSleep
        {
            get { return m_MostLikelyOfSleep; }
            set { m_MostLikelyOfSleep = value; }
        }

        public void UpdateTotalSleep(DateTime i_StartSleep, DateTime i_EndSleep)
        {
            TimeSpan diff = i_EndSleep - i_StartSleep;
            int number;
            int.TryParse(diff.TotalMinutes.ToString(), out number);
            this.TotalSleep += number;
        }

        public void UpdateAllHuoresOfSleep(DateTime i_StartSleep, DateTime i_EndSleep)
        {
            while (i_StartSleep <= i_EndSleep)
            {
                string time = i_StartSleep.ToString("HH:mm");//Save minute as HH:mm format

                if (!m_MostLikelyOfSleep.ContainsKey(time))
                {
                    this.m_MostLikelyOfSleep[time] = 1;
                }
                else
                {
                    this.m_MostLikelyOfSleep[time] += 1;
                }
                if (this.ValueOfMaxMinute < this.m_MostLikelyOfSleep[time])
                {
                    this.MaxMinute = time;
                    this.ValueOfMaxMinute = this.m_MostLikelyOfSleep[time];
                }

                i_StartSleep = i_StartSleep.AddMinutes(1);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeExam_DeepInstinct
{
    public class Solution
    {
        private string m_TextFilePath;

        private Dictionary<string, Guard> m_InformationFromTextAboutEveryGuard = new Dictionary<string, Guard>();

        public void RunChallenge(string i_Path)
        {
            m_TextFilePath = i_Path;
            extractFromFile();// Extract all the data from the input file to the Guards obj in the Dictionary.
            int max = 0;
            Guard mostSleepyGuard = new Guard();
            foreach (var kvp in m_InformationFromTextAboutEveryGuard)
            {
                if (kvp.Value.TotalSleep > max)
                {
                    max = kvp.Value.TotalSleep;
                    mostSleepyGuard = kvp.Value;
                }
            }

            Console.WriteLine("Guard " + mostSleepyGuard.ID + " is most likely to be asleep in " + mostSleepyGuard.MaxMinute);
        }


        private void extractFromFile()
        {
            try
            {
                if (!File.Exists(this.m_TextFilePath))
                    throw new FileNotFoundException();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }

            string[] allTextFromFile = File.ReadAllLines(this.m_TextFilePath);
            StreamReader file = new StreamReader(m_TextFilePath);
            Guard currentGuard = null;
            string line;

            while ((line = file.ReadLine()) != null)
            {
                List<string> infoOfOneLine = extractInfoFromOneLine(line);
                if (infoOfOneLine.Contains("Guard"))//if Guard line
                {
                    extractFromGuardLine(infoOfOneLine, ref currentGuard);
                }
                if (infoOfOneLine.Contains("falls"))//Line when guard fall asleep
                {
                    extractHouresOfSleep(infoOfOneLine, line, ref currentGuard, file);
                }
            }

            file.Close();
        }

        private DateTime extractDateAndTime(string i_Line)
        {
            string dateAndTime = i_Line.Split(']').ToList()[0];
            dateAndTime = new string(dateAndTime.SkipWhile(c => c == '[').ToArray());
            DateTime parsedDate = DateTime.Parse(dateAndTime);
            return parsedDate;
        }

        private List<string> extractInfoFromOneLine(string i_Line)
        {
            List<string> info = i_Line.Split(' ').ToList();
            return info;
        }

        private void extractFromGuardLine(List<string> i_SplitedLine, ref Guard i_CurrentGuard)
        {
            string ID = i_SplitedLine[3];

            if (!m_InformationFromTextAboutEveryGuard.ContainsKey(ID))
            {
                Guard guard = new Guard();
                guard.ID = ID;
                i_CurrentGuard = guard;
                m_InformationFromTextAboutEveryGuard.Add(ID, guard);
            }
            else
            {
                m_InformationFromTextAboutEveryGuard.TryGetValue(ID, out i_CurrentGuard);
            }
        }

        private void extractHouresOfSleep(List<string> i_SplitedLine, string i_Line, ref Guard i_CurrentGuard, StreamReader i_File)
        {
            try
            {
                if (i_CurrentGuard == null)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }

            DateTime startSleep = extractDateAndTime(i_Line);
            string line = i_File.ReadLine();// to the next line
            checkValidLineOfWakeUp(line);
            DateTime endSleep = extractDateAndTime(line);
            endSleep = endSleep.AddMinutes(-1); // beacuse in the last minute he's wake up

            i_CurrentGuard.UpdateTotalSleep(startSleep, endSleep);
            i_CurrentGuard.UpdateAllHuoresOfSleep(startSleep, endSleep);
        }

        private void checkValidLineOfWakeUp(string i_Line)//check if the second line after "fall asleep" line, is valid line
        {
            List<string> infoOfOneLine = extractInfoFromOneLine(i_Line);
            try
            {
                if (!infoOfOneLine.Contains("wakes"))
                {
                    throw new Exception("Invalid text from file");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

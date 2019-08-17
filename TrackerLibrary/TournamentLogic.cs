using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        // Create our matchups
        // Order our list randomly
        // Check if it is the proper size - if not, add in byes (auto-wins) - Proper participant size = 2^n
        // Create our first round of matchups
        // Create every round after that -- 8 matchups - 4 matchups - 2 matchups - 1 matchup

        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);

            int roundCount = FindNumberOfRounds(randomizedTeams.Count);
            int byeCount = FindNumberOfByes(roundCount, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(byeCount, randomizedTeams));
            CreateRemainingRounds(model, roundCount);
        }

        private static void CreateRemainingRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel curMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel matchup in previousRound)
                {
                    curMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = matchup });

                    if (curMatchup.Entries.Count > 1)
                    {
                        curMatchup.MatchupRound = round;
                        currentRound.Add(curMatchup);
                        curMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;
                currentRound = new List<MatchupModel>();
                round += 1;
            }
        }

        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel cur = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                cur.Entries.Add(new MatchupEntryModel { TeamCompeting = team });

                if (byes > 0 || cur.Entries.Count > 1)
                {
                    cur.MatchupRound = 1;
                    output.Add(cur);
                    cur = new MatchupModel();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }
            }

            return output;
        }

        private static int FindNumberOfByes(int rounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;

            return output;
        }

        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while (val < teamCount)
            {
                output += 1;
                val *= 2;
            }

            return output;
        }
            

        private static List<TeamModel> RandomizeTeamOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}

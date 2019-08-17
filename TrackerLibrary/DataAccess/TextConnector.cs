using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataSource
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";

        // TODO - Wire up the CreatePrize for text files
        public void CreatePrize(PrizeModel model)
        {
            // Load text file
            // Convert text to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find maxID
            int currentID = 1;

            if (prizes.Count > 0)
            {
                currentID = prizes.OrderByDescending(x => x.ID).First().ID + 1;
            }

            model.ID = currentID;

            // Add new record with the new ID (maxID+1)
            prizes.Add(model);

            // Convert prizes to List<string>
            // Save List<string> to text file
            prizes.SaveToPrizeFile(PrizesFile);
        }

        public void CreatePerson(PersonModel model)
        {
            // Load text file
            // Convert text to List<PersonModel>
            List<PersonModel> people = GetPerson_All();

            // Find maxID
            int currentID = 1;

            if (people.Count > 0)
            {
                currentID = people.OrderByDescending(x => x.ID).First().ID + 1;
            }

            model.ID = currentID;

            // Add new record with the new ID (maxID+1)
            people.Add(model);

            // Convert people to List<string>
            // Save List<string> to text file
            people.SaveToPeopleFile(PeopleFile);
        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public void CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            // Find maxID
            int currentID = 1;

            if (teams.Count > 0)
            {
                currentID = teams.OrderByDescending(x => x.ID).First().ID + 1;
            }

            model.ID = currentID;

            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);
        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamFile, PeopleFile, PrizesFile);

            int currentID = 1;

            if (tournaments.Count > 0)
            {
                currentID = tournaments.OrderByDescending(x => x.ID).First().ID + 1;
            }

            model.ID = currentID;

            tournaments.Add(model);

            tournaments.SaveToTournamentFile(TournamentFile);
        }
    }
}

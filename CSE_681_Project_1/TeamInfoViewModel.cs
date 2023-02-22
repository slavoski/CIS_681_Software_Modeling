using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace CSE_681_Project_1
{
	public class TeamInfoViewModel : BaseViewModel
	{
		#region member variables

		private TeamInfo _teamInfo;

		private List<string> images = new List<string>()
		{
			"https://content.sportslogos.net/logos/7/177/full/arizona_cardinals_logo_helmet_20059980.png", // blank
			"https://content.sportslogos.net/logos/7/177/full/arizona_cardinals_logo_helmet_20059980.png", // Cardinals
			"https://content.sportslogos.net/logos/7/173/full/3438_atlanta_falcons-helmet-2020.png", // Falcons
			"https://content.sportslogos.net/logos/7/153/full/317.png", // Ravens
			"https://content.sportslogos.net/logos/7/149/full/buffalo_bills_logo_helmet_2021_sportslogosnet-4815.png", //Bills
			"https://content.sportslogos.net/logos/7/174/full/8708_carolina_panthers-helmet-2012.png", // Panthers
			"https://content.sportslogos.net/logos/7/169/full/a8zlwo4e91wi3srhc2f18l5w6.png", // Bears
			"https://content.sportslogos.net/logos/7/155/full/7855_cleveland_browns-primary-2015.png", // browns
			"https://content.sportslogos.net/logos/7/165/full/o1wgfvp4km9109vj1p4fkq212.png", //cowboys
			"https://content.sportslogos.net/logos/7/161/full/gvbg3heqpbbmfh36ece0v7b67.png", // Broncos
			"https://content.sportslogos.net/logos/7/170/full/5885_detroit_lions-helmet-2017.png", // Lions
			"https://content.sportslogos.net/logos/7/171/full/559.png", // Packers
			"https://content.sportslogos.net/logos/7/166/full/80fw425vg3404shgkeonlmsgf.png", // Giants
			"https://content.sportslogos.net/logos/7/158/full/2ufhm4emsi0ga30iwqjru46mm.png", // Colts
			"https://content.sportslogos.net/logos/7/159/full/3944_jacksonville_jaguars-helmet-2018.png", // Jaguars
			"https://content.sportslogos.net/logos/7/162/full/mbtef36mxmdxosrpvl4hf3e8i.png", // chiefs
			"https://content.sportslogos.net/logos/7/150/full/5586_miami_dolphins-helmet-2018.png", // dolphins
			"https://content.sportslogos.net/logos/7/172/full/4790_minnesota_vikings-helmet-2013.png", // vikings
			"https://content.sportslogos.net/logos/7/151/full/ki7ghgfrfy7ufmkza3fetxz64.png", // patriots
			"https://content.sportslogos.net/logos/7/175/full/910.png", // saints
			"https://content.sportslogos.net/logos/7/152/full/8989_new_york_jets-helmet-2019.png", // jets
			"https://content.sportslogos.net/logos/7/6708/full/4168_las_vegas_raiders-helmet-2020.png", // raiders
			"https://content.sportslogos.net/logos/7/167/full/58p6tm0b3zr4dsrhevp12uva4.png", // eagles
			"https://content.sportslogos.net/logos/7/156/full/972.png", // Steelers
			"https://content.sportslogos.net/logos/7/6446/full/4290_los_angeles__chargers-helmet-2020.png", // chargers
			"https://content.sportslogos.net/logos/7/180/full/3736_seattle_seahawks-helmet-2012.png", // Seahawks
			"https://content.sportslogos.net/logos/7/179/full/vj3mzax8z0hvgafjtsccwcqde.png",        // 49ers
			"https://content.sportslogos.net/logos/7/5941/full/3521_los_angeles_rams-helmet-2020.png", // Rams
			"https://content.sportslogos.net/logos/7/176/full/5717_tampa_bay_buccaneers-helmet-2020.png", // Buccaneers
			"https://content.sportslogos.net/logos/7/160/full/6034_tennessee_titans-helmet-2018.png", // Titans
			"https://content.sportslogos.net/logos/7/6832/full/washington_commanders_logo_helmet_2022_sportslogosnet-9702.png", // Commanders
			"https://content.sportslogos.net/logos/7/154/full/ftn3942al0p6xnzh9lv9v11hl.png", // Bengals
			"https://content.sportslogos.net/logos/7/157/full/houston_texans_logo_helmet_2022_sportslogosnet-4210.png", // Texans
		};

		#endregion member variables

		#region properties

		public BitmapImage BitmapImage
		{
			get;
			set;
		}

		public int FirstDowns => _teamInfo.firstDowns;
		public int FourthDownAttempts => _teamInfo.fourthDownAtt;
		public int FourthDownConversions => _teamInfo.fourthDownConver;
		public int FumblesLost => _teamInfo.fumblesLost;
		public ulong GameCode => _teamInfo.gameCode;
		public string GameDate => _teamInfo.gameDate;
		public int InterceptionsThrown => _teamInfo.interceptionsThrown;
		public int PassAttempts => _teamInfo.passAtt;
		public int PassesCompleted => _teamInfo.passComp;
		public int PassYards => _teamInfo.passYds;
		public int Penalties => _teamInfo.penalties;
		public int PenaltyYards => _teamInfo.penaltYds;
		public int RushAttempts => _teamInfo.rushAtt;
		public int RushYards => _teamInfo.rushYds;
		public int Score => _teamInfo.score;
		public ulong StatIDCode => _teamInfo.statIdCode;
		public int TeamCode => _teamInfo.teamCode;
		public int ThirdDownAttempts => _teamInfo.thridDownAtt;
		public int ThirdDownConversions => _teamInfo.thirdDownConver;

		public int TimePossed => _teamInfo.timePoss;

		#endregion properties

		public TeamInfoViewModel(TeamInfo teamInfo)
		{
			_teamInfo = teamInfo;
			BitmapImage = new BitmapImage(new Uri(images[_teamInfo.teamCode]));
		}
	}
}
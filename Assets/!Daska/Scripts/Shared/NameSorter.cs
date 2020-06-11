﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NameSorter : MonoBehaviour {
	private string saveFilePath;
	private SaveObject saveObject = new SaveObject();

	private void Awake() => saveFilePath = Application.persistentDataPath + "/SaveObject.json";

	private void Start() {
		for (int i = 0; i < saveObject.namesList.Count; i++) {
			saveObject.namesList[i] = saveObject.namesList[i].Trim();
		}
		SaveData();
	}

	internal void SaveData() {
		string dataAsJson = JsonUtility.ToJson(saveObject, true);
		File.WriteAllText(saveFilePath, dataAsJson);
	}

	internal void LoadData() {
		if (File.Exists(saveFilePath)) {
			string dataAsJson = File.ReadAllText(saveFilePath);
			JsonUtility.FromJsonOverwrite(dataAsJson, saveObject);
		}
	}

	[Serializable]
	private class SaveObject {
		public List<string> namesList = new List<string>() {
		"Absolutno			",
"Achernar			",
"Achird				",
"Acrab				",
"Acrux				",
"Acubens			",
"Adhafera			",
"Adhara				",
"Adhil				",
"Ain				",
"Ainalrami			",
"Aladfar			",
"Alamak				",
"Alasia				",
"Alathfar			",
"Albaldah			",
"Albali				",
"Albireo			",
"Alchiba			",
"Alcor				",
"Alcyone			",
"Aldebaran			",
"Alderamin			",
"Aldhanab			",
"Aldhibah			",
"Aldulfin			",
"Alfirk				",
"Algedi				",
"Algenib			",
"Algieba			",
"Algol				",
"Algorab			",
"Alhena				",
"Alioth				",
"Aljanah			",
"Alkaid				",
"Al Kalb al Rai		",
"Alkalurops			",
"Alkaphrah			",
"Alkarab			",
"Alkes				",
"Almaaz				",
"Almach				",
"Al Minliar al Asad	",
"Alnair				",
"Alnasl				",
"Alnilam			",
"Alnitak			",
"Alniyat			",
"Alphard			",
"Alphecca			",
"Alpheratz			",
"Alpherg			",
"Alrakis			",
"Alrescha			",
"Alruba				",
"Alsafi				",
"Alsciaukat			",
"Alsephina			",
"Alshain			",
"Alshat				",
"Altair				",
"Altais				",
"Alterf				",
"Aludra				",
"Alula Australis	",
"Alula Borealis		",
"Alya				",
"Alzirr				",
"Amadioha			",
"Amansinaya			",
"Anadolu			",
"Ancha				",
"Angetenar			",
"Aniara				",
"Ankaa				",
"Anser				",
"Antares			",
"Arcalis			",
"Arcturus			",
"Arkab Posterior	",
"Arkab Prior		",
"Arneb				",
"Ascella			",
"Asellus Australis	",
"Asellus Borealis	",
"Ashlesha			",
"Asellus Primus		",
"Asellus Secundus	",
"Asellus Tertius	",
"Aspidiske			",
"Asterope, Sterope	",
"Atakoraka			",
"Athebyne			",
"Atik				",
"Atlas				",
"Atria				",
"Avior				",
"Axolotl			",
"Ayeyarwady			",
"Azelfafage			",
"Azha				",
"Azmidi				",
"Baekdu				",
"Barnard's Star		",
"Baten Kaitos		",
"Beemim				",
"Beid				",
"Belel				",
"Belenos			",
"Bellatrix			",
"Berehinya			",
"Betelgeuse			",
"Bharani			",
"Bibha				",
"Biham				",
"Bosona				",
"Botein				",
"Brachium			",
"Bubup				",
"Buna				",
"Bunda				",
"Canopus			",
"Capella			",
"Caph				",
"Castor				",
"Castula			",
"Cebalrai			",
"Ceibo				",
"Celaeno			",
"Cervantes			",
"Chalawan			",
"Chamukuy			",
"Chaophraya			",
"Chara				",
"Chason				",
"Chechia			",
"Chertan			",
"Citadelle			",
"Citala				",
"Cocibolca			",
"Copernicus			",
"Cor Caroli			",
"Cujam				",
"Cursa				",
"Dabih				",
"Dalim				",
"Deneb				",
"Deneb Algedi		",
"Denebola			",
"Diadem				",
"Dingolay			",
"Diphda				",
"Diwo				",
"Diya				",
"Dofida				",
"Dombay				",
"Dschubba			",
"Dubhe				",
"Dziban				",
"Ebla				",
"Edasich			",
"Electra			",
"Elgafar			",
"Elkurud			",
"Elnath				",
"Eltanin			",
"Emiw				",
"Enif				",
"Errai				",
"Fafnir				",
"Fang				",
"Fawaris			",
"Felis				",
"Felixvarela		",
"Flegetonte			",
"Fomalhaut			",
"Formosa			",
"Franz				",
"Fulu				",
"Funi				",
"Fumalsamakah		",
"Furud				",
"Fuyue				",
"Gacrux				",
"Gakyid				",
"Garnet Star		",
"Giausar			",
"Gienah				",
"Ginan				",
"Gloas				",
"Gomeisa			",
"Graffias			",
"Grumium			",
"Gudja				",
"Gumala				",
"Guniibuu			",
"Hadar				",
"Haedus				",
"Hamal				",
"Hassaleh			",
"Hatysa				",
"Helvetios			",
"Heze				",
"Hoggar				",
"Homam				",
"Horna				",
"Hunahpu			",
"Hunor				",
"Iklil				",
"Illyrian			",
"Imai				",
"Intercrus			",
"Inquill			",
"Intan				",
"Irena				",
"Itonda				",
"Izar				",
"Jabbah				",
"Jishui				",
"Kaffaljidhma		",
"Kakkab				",
"Kalausi			",
"Kamui				",
"Kang				",
"Karaka				",
"Kaus Australis		",
"Kaus Borealis		",
"Kaus Media			",
"Kaveh				",
"Kekouan			",
"Keid				",
"Khambalia			",
"Kitalpha			",
"Kochab				",
"Koeia				",
"Koit				",
"Kornephoros		",
"Kraz				",
"Kuma				",
"Kurhah				",
"La Superba			",
"Larawag			",
"Lerna				",
"Lesath				",
"Libertas			",
"Lich				",
"Liesma				",
"Lilii Borea		",
"Lionrock			",
"Lucilinburhuc		",
"Lusitania			",
"Maasym				",
"Macondo			",
"Mago				",
"Mahasim			",
"Mahsati			",
"Maia				",
"Malmok				",
"Marfark			",
"Marfik				",
"Markab				",
"Markeb				",
"Marohu				",
"Marsic				",
"Matar				",
"Mebsuta			",
"Megrez				",
"Meissa				",
"Mekbuda			",
"Meleph				",
"Menkalinan			",
"Menkar				",
"Menkent			",
"Menkib				",
"Merak				",
"Merga				",
"Meridiana			",
"Merope				",
"Mesarthim			",
"Miaplacidus		",
"Mimosa				",
"Minchir			",
"Minelauva			",
"Mintaka			",
"Mira				",
"Mirach				",
"Miram				",
"Mirfak				",
"Mirzam				",
"Misam				",
"Mizar				",
"Moldoveanu			",
"Monch				",
"Montuno			",
"Morava				",
"Moriah				",
"Mothallah			",
"Mouhoun			",
"Mpingo				",
"Muliphein			",
"Muphrid			",
"Muscida			",
"Musica				",
"Muspelheim			",
"Nahn				",
"Naledi				",
"Naos				",
"Nash				",
"Nashira			",
"Nasti				",
"Natasha			",
"Navi				",
"Nekkar				",
"Nembus				",
"Nenque				",
"Nervia				",
"Nihal				",
"Nikawiy			",
"Nosaxa				",
"Nunki				",
"Nusakan			",
"Nushagak			",
"Nyamien			",
"Ogma				",
"Okab				",
"Paikauhale			",
"Parumleo			",
"Peacock			",
"Petra				",
"Phact				",
"Phecda				",
"Pherkad			",
"Phoenicia			",
"Piautos			",
"Pincoya			",
"Pipoltr			",
"Pipirima			",
"Pleione			",
"Poerava			",
"Polaris			",
"Polaris Australis	",
"Polis				",
"Pollux				",
"Porrima			",
"Praecipua			",
"Prima Hyadum		",
"Procyon			",
"Propus				",
"Proxima Centauri	",
"Ran				",
"Rapeto				",
"Rasalas			",
"Rasalgethi			",
"Rasalhague			",
"Rastaban			",
"Regor				",
"Regulus			",
"Revati				",
"Rigel				",
"Rigil Kentaurus	",
"Rosaliadecastro	",
"Rotanev			",
"Ruchbah			",
"Rukbat				",
"Sabik				",
"Saclateni			",
"Sadachbia			",
"Sadalbari			",
"Sadalmelik			",
"Sadalsuud			",
"Sadr				",
"Sagarmatha			",
"Saiph				",
"Salm				",
"Samaya				",
"Sansuna			",
"Sargas				",
"Sarin				",
"Sarir				",
"Sceptrum			",
"Scheat				",
"Schedar			",
"Secunda Hyadum		",
"Segin				",
"Seginus			",
"Sham				",
"Shama				",
"Sharjah			",
"Shaula				",
"Sheliak			",
"Sheratan			",
"Sika				",
"Sirius				",
"Situla				",
"Skat				",
"Solaris			",
"Spica				",
"Sterrennacht		",
"Stribor			",
"Sualocin			",
"Subra				",
"Suhail				",
"Sulafat			",
"Syrma				",
"Tabit				",
"Taika				",
"Taiyangshou		",
"Taiyi				",
"Talitha			",
"Tangra				",
"Tania Australis	",
"Tania Borealis		",
"Tapecue			",
"Tarazed			",
"Tarf				",
"Taygeta			",
"Tegmine			",
"Tejat				",
"Terebellum			",
"Tevel				",
"Thabit				",
"Theemin			",
"Thuban				",
"Tiaki				",
"Tianguan			",
"Tianyi				",
"Timir				",
"Tislit				",
"Titawin			",
"Tojil				",
"Toliman			",
"Tonatiuh			",
"Torcular			",
"Tuiren				",
"Tupa				",
"Tupi				",
"Tureis				",
"Ukdah				",
"Uklun				",
"Unukalhai			",
"Unurgunite			",
"Uruk				",
"Vega				",
"Veritate			",
"Vindemiatrix		",
"Wasat				",
"Wazn				",
"Wezen				",
"Wurren				",
"Xamidimura			",
"Xihe				",
"Xuange				",
"Yed Posterior		",
"Yed Prior			",
"Yildun				",
"Zaniah				",
"Zaurak				",
"Zavijava			",
"Zhang				",
"Zibal				",
"Zosma				",
"Zubenelgenubi		",
"Zubenelhakrabi		",
"Zubeneschamali		"};
	}
}
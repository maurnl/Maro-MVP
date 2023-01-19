﻿using System;
using System.Collections.Generic;
using Views;
using Model;

namespace Presenter
{
	public class MainPresenter
	{
		//*************************************************	
		
		private readonly IMainView _iMainView;		
		List<Character> characters;
		int id;
		string date;
		
		//*************************************************	
		
		//-----------------------------------------------------
		//------------------ [ PROPERTIES ]
		//-----------------------------------------------------
		
		public List<Character> Characters
		{
			get { return characters; }
			set { characters = value; }
		}

		public int ID
		{
			get { return id; }
			set { id = value; }
		}
		
		//-----------------------------------------------------
		
		public MainPresenter(IMainView iMainView)
		{
			_iMainView = iMainView;
			
			characters = new List<Character>();
			id = 0;
			
			iMainView.RetrieveData += (o, e) =>
			{				
				DateRetriever retriever = new DateRetriever();
				date = retriever.GetMaroDate();
				Retrieve(date);
			};			
			
		}		

		//-----------------------------------------------------
		//------------------ [ METHODS ]
		//-----------------------------------------------------
		
		public void Retrieve(string date)
		{			
			_iMainView.Lbl_MaroDate = date;
		}
	}
}

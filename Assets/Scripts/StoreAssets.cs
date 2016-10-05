using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Soomla.Store.GalaxyDefender
{
	
	/// <summary>
	/// This class defines our game's economy, which includes virtual goods, virtual currencies
	/// and currency packs, virtual categories
	/// </summary>
	public class GalaxyDefenderAssets : IStoreAssets
	{
		
		/// <summary>
		/// see parent.
		/// </summary>
		public int GetVersion ()
		{
			return 10;
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrency[] GetCurrencies ()
		{
			return new VirtualCurrency[]{};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualGood[] GetGoods ()
		{
			return new VirtualGood[] {NO_ADS};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrencyPack[] GetCurrencyPacks ()
		{
			return new VirtualCurrencyPack[] {};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCategory[] GetCategories ()
		{
			return new VirtualCategory[]{GENERAL_CATEGORY};
		}
		
		/** Static Final Members **/
		
		public const string NO_ADS_PRODUCT_ID = "no_ads";
		
		/** Virtual Currencies **/
		
		/** Virtual Categories **/	
		
		/** LifeTimeVGs **/
		// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
		public static LifetimeVG NO_ADS = new LifetimeVG (
			"No Ads", 														// name
			"Removes Ads from the game forever!",				 			// description
			"no_ads",														// item id
			new PurchaseWithMarket (NO_ADS_PRODUCT_ID, 1.39));	// the way this virtual good is purchased
				
		public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory (
			"General", new List<string> (new string[] {})
		);
	}

}
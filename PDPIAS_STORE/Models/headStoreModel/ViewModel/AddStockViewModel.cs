using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PDPIAS_STORE.Models.headStoreModel.ViewModel
{
    public class AddStockViewModel
    {
        public List<LstChemical> chemicals = new List<LstChemical>();
    }

    public class LstChemical
    {
        public int ChemicalId;
        public string Name;
    }
}
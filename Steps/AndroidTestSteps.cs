using SeleniumWithSpecflow.Utlities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumWithSpecflow.Steps
{
    [Binding]
    class AndroidTestSteps
    {

        [Given(@"I lauch my android application")]
        public void GivenILauchMyAndroidApplication()
        {
          //  Drivers.LaunchDriver("Android Driver");
        }

    }
}

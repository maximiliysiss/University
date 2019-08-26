using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Typography.Forms.CreateEdit;
using Typography.Forms.List;

namespace Typography.Services
{
    public class GlobalContext
    {
        static StandardKernel standardKernel;
        public static StandardKernel StandardKernel
        {
            get
            {
                if (standardKernel != null)
                    return standardKernel;

                standardKernel = new StandardKernel();
                standardKernel.Load(Assembly.GetExecutingAssembly());
                return standardKernel;
            }
        }

        static FactoryGenerator<Form> factoryGenerator;
        public static FactoryGenerator<Form> FactoryGeneratorCreateEdit
        {
            get
            {
                if (factoryGenerator != null)
                    return factoryGenerator;
                factoryGenerator = new FactoryGenerator<Form>();
                var context = standardKernel.Get<IDatabaseContext>();
                factoryGenerator.AddFor(new TypographyForm(context, () => context.Typographies.ToList(), "Typography"))
                    .Where(x => (x[0] as string) == "Typography");
                factoryGenerator.AddFor(new CreateEditBaseForm<Models.Distribution>(context, () => context.Distributions.ToList(), "Distribution"))
                    .Where(x => (x[0] as string) == "Distribution");
                factoryGenerator.AddFor(new PostOfficerForm(context, () => context.PostOfficers.ToList(), "PostOfficer"))
                    .Where(x => (x[0] as string) == "PostOfficer");
                return factoryGenerator;
            }
        }

        static FactoryGenerator<Form> factoryGeneratorList;
        public static FactoryGenerator<Form> FactoryGeneratorList
        {
            get
            {
                if (factoryGeneratorList != null)
                    return factoryGeneratorList;
                factoryGeneratorList = new FactoryGenerator<Form>();
                var context = StandardKernel.Get<IDatabaseContext>();
                factoryGeneratorList.AddFor(new ListBaseForm<Models.Typography>(context, () => context.Typographies.ToList(), "Typography"))
                    .Where(x => (x[0] as string) == "Typography");
                factoryGeneratorList.AddFor(new ListBaseForm<Models.Distribution>(context, () => context.Distributions.ToList(), "Distribution"))
                    .Where(x => (x[0] as string) == "Distribution");
                factoryGeneratorList.AddFor(new ListBaseForm<Models.PostOfficer>(context, () => context.PostOfficers.ToList(), "PostOfficer"))
                    .Where(x => (x[0] as string) == "PostOfficer");
                return factoryGeneratorList;
            }
        }

    }
}

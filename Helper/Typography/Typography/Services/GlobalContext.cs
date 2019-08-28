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
using Typography.Models;

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

        static IDatabaseContext context;
        public static IDatabaseContext DatabaseContext => context ?? (context = StandardKernel.Get<IDatabaseContext>());

        static FactoryGenerator<Form, object, string> factoryGetCreateForm;

        public static FactoryGenerator<Form, object, string> FactoryGeneratorCreateForm
        {
            get
            {
                if (factoryGetCreateForm != null)
                    return factoryGetCreateForm;
                factoryGetCreateForm = new FactoryGenerator<Form, object, string>();
                factoryGetCreateForm.AddFor(() => new TypographyForm(DatabaseContext, DatabaseContext.Typographies, "Typography"))
                    .Where("Typography");
                factoryGetCreateForm.AddFor(() => new DistributionForm(DatabaseContext, DatabaseContext.Distributions, "Distribution"))
                    .Where("Distribution");
                factoryGetCreateForm.AddFor(() => new PaperForm(DatabaseContext, DatabaseContext.Papers, "Paper"))
                    .Where("Paper");
                factoryGetCreateForm.AddFor(() => new PostOfficerForm(DatabaseContext, DatabaseContext.PostOfficers, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetCreateForm.AddFor(() => new ReleaseForm(DatabaseContext, DatabaseContext.Releases, "Release"))
                    .Where("Release");
                return factoryGetCreateForm;
            }
        }

        static FactoryGenerator<Form, object, string> factoryGetListForm;

        public static FactoryGenerator<Form, object, string> FactoryGeneratorListForm
        {
            get
            {
                if (factoryGetListForm != null)
                    return factoryGetListForm;
                factoryGetListForm = new FactoryGenerator<Form, object, string>();
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Typography>(DatabaseContext, DatabaseContext.Typographies, "Typography"))
                    .Where("Typography");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Distribution>(DatabaseContext, DatabaseContext.Distributions, "Distribution"))
                    .Where("Distribution");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.PostOfficer>(DatabaseContext, DatabaseContext.PostOfficers, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Paper>(DatabaseContext, DatabaseContext.Papers, "Paper"))
                    .Where("Paper");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Release>(DatabaseContext, DatabaseContext.Releases, "Release"))
                    .Where("Release");
                return factoryGetListForm;
            }
        }

        static FactoryGenerator<ISelectForm, object, string> factoryGetSelectListForm;

        public static FactoryGenerator<ISelectForm, object, string> FactoryGeneratorSelectListForm
        {
            get
            {
                if (factoryGetSelectListForm != null)
                    return factoryGetSelectListForm;
                factoryGetSelectListForm = new FactoryGenerator<ISelectForm, object, string>();
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Typography>(DatabaseContext, DatabaseContext.Typographies, "Typography") { IsSelect = true })
                    .Where("Typography");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Distribution>(DatabaseContext, DatabaseContext.Distributions, "Distribution") { IsSelect = true })
                    .Where("Distribution");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.PostOfficer>(DatabaseContext, DatabaseContext.PostOfficers, "PostOfficer") { IsSelect = true })
                    .Where("PostOfficer");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Paper>(DatabaseContext, DatabaseContext.Papers, "Paper") { IsSelect = true })
                    .Where("Paper");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Release>(DatabaseContext, DatabaseContext.Releases, "Release") { IsSelect = true })
                    .Where("Release");
                return factoryGetSelectListForm;
            }
        }

        static FactoryGenerator<Form, object, string> factoryGetEditForm;

        public static FactoryGenerator<Form, object, string> FactoryGetEditForm
        {
            get
            {
                if (factoryGetEditForm != null)
                    return factoryGetEditForm;
                factoryGetEditForm = new FactoryGenerator<Form, object, string>();
                factoryGetEditForm.AddFor((x) => new TypographyForm(DatabaseContext, x as Models.Typography, DatabaseContext.Typographies, "Typography"))
                    .Where("Typography");
                factoryGetEditForm.AddFor((x) => new DistributionForm(DatabaseContext, x as Models.Distribution, DatabaseContext.Distributions, "Distribution"))
                    .Where("Distribution");
                factoryGetEditForm.AddFor((x) => new PostOfficerForm(DatabaseContext, x as PostOfficer, DatabaseContext.PostOfficers, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetEditForm.AddFor((x) => new PaperForm(DatabaseContext, x as Paper, DatabaseContext.Papers, "Paper"))
                    .Where("Paper");
                factoryGetEditForm.AddFor((x) => new ReleaseForm(DatabaseContext, x as Release, DatabaseContext.Releases, "Release"))
                    .Where("Release");
                return factoryGetEditForm;
            }
        }
    }
}

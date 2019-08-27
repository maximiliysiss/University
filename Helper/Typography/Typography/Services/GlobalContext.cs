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

        static FactoryGenerator<Form, object, string> factoryGetCreateForm;

        public static FactoryGenerator<Form, object, string> FactoryGeneratorCreateForm
        {
            get
            {
                if (factoryGetCreateForm != null)
                    return factoryGetCreateForm;
                factoryGetCreateForm = new FactoryGenerator<Form, object, string>();
                var context = standardKernel.Get<IDatabaseContext>();
                factoryGetCreateForm.AddFor(() => new TypographyForm(context, context.Typographies, "Typography"))
                    .Where("Typography");
                factoryGetCreateForm.AddFor(() => new DistributionForm(context, context.Distributions, "Distribution"))
                    .Where("Distribution");
                factoryGetCreateForm.AddFor(() => new PaperForm(context, context.Papers, "Paper"))
                    .Where("Paper");
                factoryGetCreateForm.AddFor(() => new PostOfficerForm(context, context.PostOfficers, "PostOfficer"))
                    .Where("PostOfficer");
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
                var context = StandardKernel.Get<IDatabaseContext>();
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Typography>(context, context.Typographies, "Typography"))
                    .Where("Typography");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Distribution>(context, context.Distributions, "Distribution"))
                    .Where("Distribution");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.PostOfficer>(context, context.PostOfficers, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Paper>(context, context.Papers, "Paper"))
                    .Where("Paper");
                factoryGetListForm.AddFor(() => new ListBaseForm<Models.Release>(context, context.Releases, "Release"))
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
                var context = StandardKernel.Get<IDatabaseContext>();
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Typography>(context, context.Typographies, "Typography") { IsSelect = true })
                    .Where("Typography");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Distribution>(context, context.Distributions, "Distribution") { IsSelect = true })
                    .Where("Distribution");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.PostOfficer>(context, context.PostOfficers, "PostOfficer") { IsSelect = true })
                    .Where("PostOfficer");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Paper>(context, context.Papers, "Paper") { IsSelect = true })
                    .Where("Paper");
                factoryGetSelectListForm.AddFor(() => new ListBaseForm<Models.Release>(context, context.Releases, "Release") { IsSelect = true })
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
                var context = StandardKernel.Get<IDatabaseContext>();
                factoryGetEditForm.AddFor((x) => new TypographyForm(context, x as Models.Typography, context.Typographies, "Typography"))
                    .Where("Typography");
                factoryGetEditForm.AddFor((x) => new DistributionForm(context, x as Models.Distribution, context.Distributions, "Distribution"))
                    .Where("Distribution");
                factoryGetEditForm.AddFor((x) => new PostOfficerForm(context, x as PostOfficer, context.PostOfficers, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetEditForm.AddFor((x) => new PaperForm(context, x as Paper, context.Papers, "Paper"))
                    .Where("Paper");
                return factoryGetEditForm;
            }
        }
    }
}

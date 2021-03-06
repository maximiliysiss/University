﻿using Ninject;
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
    /// <summary>
    /// Глобальный контекст
    /// </summary>
    public class GlobalContext
    {
        /// <summary>
        /// Ядро для Ninject (кэш)
        /// </summary>
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

        /// <summary>
        /// Подключение к БД (кэш)
        /// </summary>
        static IDatabaseContext context;
        public static IDatabaseContext DatabaseContext => context ?? (context = StandardKernel.Get<IDatabaseContext>());


        /// <summary>
        /// Фабрика для создания форм для создания (кэш)
        /// </summary>
        static FactoryGenerator<Form, object, string> factoryGetCreateForm;
        public static FactoryGenerator<Form, object, string> FactoryGeneratorCreateForm
        {
            get
            {
                if (factoryGetCreateForm != null)
                    return factoryGetCreateForm;
                factoryGetCreateForm = new FactoryGenerator<Form, object, string>();
                factoryGetCreateForm.AddFor(() => new TypographyForm(DatabaseContext, "Typography"))
                    .Where("Typography");
                factoryGetCreateForm.AddFor(() => new DistributionForm(DatabaseContext, "Distribution"))
                    .Where("Distribution");
                factoryGetCreateForm.AddFor(() => new PaperForm(DatabaseContext, "Paper"))
                    .Where("Paper");
                factoryGetCreateForm.AddFor(() => new PostOfficerForm(DatabaseContext, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetCreateForm.AddFor(() => new ReleaseForm(DatabaseContext, "Release"))
                    .Where("Release");
                return factoryGetCreateForm;
            }
        }

        /// <summary>
        /// Фабрика для создания форм со списком (кэш)
        /// </summary>
        static FactoryGenerator<IRefreshDataForm, object, string> factoryGetListForm;
        public static FactoryGenerator<IRefreshDataForm, object, string> FactoryGeneratorListForm
        {
            get
            {
                if (factoryGetListForm != null)
                    return factoryGetListForm;
                factoryGetListForm = new FactoryGenerator<IRefreshDataForm, object, string>();
                factoryGetListForm.AddFor(() => new TypographyList(DatabaseContext, DatabaseContext.Typographies, "Typography"))
                    .Where("Typography");
                factoryGetListForm.AddFor(() => new DistributionList(DatabaseContext, DatabaseContext.Distributions, "Distribution"))
                    .Where("Distribution");
                factoryGetListForm.AddFor(() => new PostOfficerList(DatabaseContext, DatabaseContext.PostOfficers, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetListForm.AddFor(() => new PaperList(DatabaseContext, DatabaseContext.Papers, "Paper"))
                    .Where("Paper");
                factoryGetListForm.AddFor(() => new ReleaseList(DatabaseContext, DatabaseContext.Releases, "Release"))
                    .Where("Release");
                return factoryGetListForm;
            }
        }

        /// <summary>
        /// Фабрика для создания форм со списком для выбора (кэш)
        /// </summary>
        static FactoryGenerator<ISelectForm, object, string> factoryGetSelectListForm;
        public static FactoryGenerator<ISelectForm, object, string> FactoryGeneratorSelectListForm
        {
            get
            {
                if (factoryGetSelectListForm != null)
                    return factoryGetSelectListForm;
                factoryGetSelectListForm = new FactoryGenerator<ISelectForm, object, string>();
                factoryGetSelectListForm.AddFor(() => new TypographyList(DatabaseContext, DatabaseContext.Typographies, "Typography") { IsSelect = true })
                    .Where("Typography");
                factoryGetSelectListForm.AddFor(() => new DistributionList(DatabaseContext, DatabaseContext.Distributions, "Distribution") { IsSelect = true })
                    .Where("Distribution");
                factoryGetSelectListForm.AddFor(() => new PostOfficerList(DatabaseContext, DatabaseContext.PostOfficers, "PostOfficer") { IsSelect = true })
                    .Where("PostOfficer");
                factoryGetSelectListForm.AddFor(() => new PaperList(DatabaseContext, DatabaseContext.Papers, "Paper") { IsSelect = true })
                    .Where("Paper");
                factoryGetSelectListForm.AddFor(() => new ReleaseList(DatabaseContext, DatabaseContext.Releases, "Release") { IsSelect = true })
                    .Where("Release");
                return factoryGetSelectListForm;
            }
        }

        /// <summary>
        /// Фабрика для создания форм для изменения и удаления (кэш)
        /// </summary>
        static FactoryGenerator<Form, object, string> factoryGetEditForm;
        public static FactoryGenerator<Form, object, string> FactoryGetEditForm
        {
            get
            {
                if (factoryGetEditForm != null)
                    return factoryGetEditForm;
                factoryGetEditForm = new FactoryGenerator<Form, object, string>();
                factoryGetEditForm.AddFor((x) => new TypographyForm(DatabaseContext, x as Models.Typography, "Typography"))
                    .Where("Typography");
                factoryGetEditForm.AddFor((x) => new DistributionForm(DatabaseContext, x as Models.Distribution, "Distribution"))
                    .Where("Distribution");
                factoryGetEditForm.AddFor((x) => new PostOfficerForm(DatabaseContext, x as PostOfficer, "PostOfficer"))
                    .Where("PostOfficer");
                factoryGetEditForm.AddFor((x) => new PaperForm(DatabaseContext, x as Paper, "Paper"))
                    .Where("Paper");
                factoryGetEditForm.AddFor((x) => new ReleaseForm(DatabaseContext, x as Release, "Release"))
                    .Where("Release");
                return factoryGetEditForm;
            }
        }
    }
}

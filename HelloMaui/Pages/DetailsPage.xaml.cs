using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloMaui.ViewModels;

namespace HelloMaui.Pages;

public partial class DetailsPage : BaseContentPage<DetailsViewModel>
{
    public DetailsPage(DetailsViewModel detailsViewModel) : base(detailsViewModel)
    {
        InitializeComponent();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using Template.Mvc4.Models;

namespace Template.Mvc4.Api
{
  public class ContactsApi
  {
    private readonly IRepository<Contact> contactRepository;

    #region -- Constructor --
    
    public ContactsApi()
      : this(new Repository<Contact>())
    {
      // If you are using Dependency Injection, you can delete the following constructor
    }

    public ContactsApi(IRepository<Contact> contactRepository)
    {
      this.contactRepository = contactRepository;
    }

    #endregion //-- Constructor --

    [WebGet(UriTemplate = "")]
    public IQueryable<Contact> GetAll()
    {
      return contactRepository.All;
    }

    [WebInvoke(UriTemplate = "", Method = "POST")]
    public Contact Create(Contact instance)
    {
      instance.ModifyDate = DateTime.Now;
      contactRepository.InsertOrUpdate(instance);
      contactRepository.Save();
      return instance;
    }

    [WebGet(UriTemplate = "{id}")]
    public Contact Get(int id)
    {
      return contactRepository.Find(id);
    }

    [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
    public Contact Update(string id, Contact instance)
    {
      instance.ModifyDate = DateTime.Now;
      contactRepository.InsertOrUpdate(instance);
      contactRepository.Save();
      return instance;
    }

    [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
    public string Delete(int id)
    {
      contactRepository.Delete(id);
      return "Contact id: " + id + " deleted.";
    }
  }
}
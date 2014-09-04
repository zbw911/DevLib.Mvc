//using Dev.Wcf.Client.Security;
//using Dev.Web.CompositionRootBase;
//using GameGroup.Application.MainBoundedContext.Activity;
//using GameGroup.Application.MainBoundedContext.Feed;
//using GameGroup.Application.MainBoundedContext.GameInfo;
//using GameGroup.Application.MainBoundedContext.ShortMessage;
//using GameGroup.Application.MainBoundedContext.UserInfo;
//using Ninject;

//namespace GameGroup.Group.CompositionRoot
//{
//    public class WcfRegisters : RegisterContextBase, IRegister
//    {
//        #region IRegister Members

//        public override void Register()
//        {
//            AppContext.UserName = " ";
//            AppContext.Password = " ";

//            #region WCF

//            this.RegServiceWith<IFeed, FeedClient>();
//            this.RegServiceWith<IActivity, ActivityClient>();
//            this.RegServiceWith<IUserInfo, UserInfoClient>();
//            this.RegServiceWith<IGameInfo, GameInfoClient>();
//            this.RegServiceWith<IShortMessage, ShortMessageClient>();

//            #endregion
//        }

//        public override IKernel Kernel { get; set; }

//        #endregion
//    }
//}
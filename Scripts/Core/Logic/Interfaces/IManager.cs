using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IManager : IContentLoadable
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods
    bool HasContentOnScene(SceneLabel sceneLabel);

    SceneLabel GetSceneLifeLabel();

    string GetLocalizedKey();

    void Initialize();

    #endregion

    #region Handlers



    #endregion
}

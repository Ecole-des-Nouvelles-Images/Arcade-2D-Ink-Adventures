using Elias.Scripts.Data;
using UnityEngine;

namespace Elias.Scripts.ObjectColorState
{
    public interface IColorState
    {
        void SetupCollision(BaseObject baseObject);
    }

    public class RedState : IColorState
    {
        public void SetupCollision(BaseObject baseObject)
        {
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectBlue"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectGreen"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectCyan"), true);
        }
    }

    public class BlueState : IColorState
    {
        public void SetupCollision(BaseObject baseObject)
        {
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectRed"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectGreen"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectYellow"), true);
        }
    }

    public class GreenState : IColorState
    {
        public void SetupCollision(BaseObject baseObject)
        {
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectRed"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectBlue"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectMagenta"), true);
        }
    }

    public class YellowState : IColorState
    {
        public void SetupCollision(BaseObject baseObject)
        {
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectBlue"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectMagenta"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectCyan"), true);
        }
    }

    public class MagentaState : IColorState
    {
        public void SetupCollision(BaseObject baseObject)
        {
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectGreen"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectYellow"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectCyan"), true);
        }
    }

    public class CyanState : IColorState
    {
        public void SetupCollision(BaseObject baseObject)
        {
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectRed"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectMagenta"), true);
            Physics2D.IgnoreLayerCollision(baseObject.gameObject.layer, LayerMask.NameToLayer("ObjectYellow"), true);
        }
    }

    public class ColorContext
    {
        private IColorState _currentState;

        public ColorContext(IColorState initialState)
        {
            _currentState = initialState;
        }

        public void SetState(IColorState newState)
        {
            _currentState = newState;
        }

        public void SetupCollision(BaseObject baseObject)
        {
            _currentState.SetupCollision(baseObject);
        }
    }
}
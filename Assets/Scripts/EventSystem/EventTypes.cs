public interface BaseEvent{}

namespace NavigationEvents{ 
    public struct LoadSceneEvent : BaseEvent {
        public short Scene;

        public LoadSceneEvent(short scene)
        {
            Scene = scene;
        }
    }

    public struct PreviousContextEvent : BaseEvent {}

    public struct LoadScreenEvent : BaseEvent {
        public string Id;
        public LoadScreenEvent(string id)
        {
            Id = id;
        }
    }

    public struct UnloadScreenEvent : BaseEvent {}
}

namespace ApplicationEvents{
    public struct StartUpFinishedEvent : BaseEvent{}
}

namespace DataEvents{ 
    public struct SetTextEvent : BaseEvent {
        public string Text;
        public SetTextEvent(string text)
        {
            Text = text;
        }
    }
}

namespace PopupEvents{
    public struct OpenPopupEvent : BaseEvent{
        public PopupAsset Popup;
        public OpenPopupEvent(PopupAsset popup)
        {
            Popup = popup;
        }
    }

    public struct ClosePopupEvent : BaseEvent{}
}

namespace GameEvents{
    public struct InitializeGameEvent : BaseEvent {}
}

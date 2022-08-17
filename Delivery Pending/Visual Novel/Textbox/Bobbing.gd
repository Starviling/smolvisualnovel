extends Polygon2D


# Declare member variables here. Examples:
var tweenBob

# Called when the node enters the scene tree for the first time.
func _ready():
	tweenBob = get_tree().create_tween().bind_node(self).set_trans(Tween.TRANS_EXPO)
	tweenBob.pause()
	tweenBob.tween_property(self, "position", Vector2(0, -20), 0.9)
	tweenBob.tween_property(self, "position", Vector2(0, 0), 0.9)
	tweenBob.tween_interval(0.1)
	tweenBob.set_loops()

func _on_DialogueUi_LineDelivered():
	var tweenFade = get_tree().create_tween().bind_node(self).set_trans(Tween.TRANS_SINE)
	tweenFade.tween_property(self, "modulate:a", 1.0, 1)
	tweenBob.play()

func _on_DialogueUi_BeginNextLine():
	var tweenFade = get_tree().create_tween().bind_node(self).set_trans(Tween.TRANS_SINE)
	tweenFade.tween_property(self, "modulate:a", 0.0, 1)
	tweenBob.stop()

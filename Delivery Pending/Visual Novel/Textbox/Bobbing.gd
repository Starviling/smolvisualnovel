extends Polygon2D


# Declare member variables here. Examples:
var tweenBob
# It seems when tweening with a single node, it does it sequentially
# Tweening other nodes with the same tween will do it simultan

# Called when the node enters the scene tree for the first time.
func _ready():
	tweenBob = get_tree().create_tween().bind_node(self).set_trans(Tween.TRANS_CIRC)
	tweenBob.pause()
	
	tweenBob.tween_property(self, "modulate:a", 1.0, 0.25)
	
	tweenBob.tween_property(self, "position", Vector2(0, -20), 0.65).set_trans(Tween.TRANS_CIRC)
	tweenBob.tween_property(self, "position", Vector2(0, 0), 0.65).set_trans(Tween.TRANS_CUBIC)
	
	tweenBob.tween_property(self, "modulate:a", 0.2, 0.25)
	
	tweenBob.tween_interval(0.45)
	tweenBob.set_loops()

func _on_DialogueUi_LineDelivered():
	#var tweenFade = get_tree().create_tween().bind_node(self).set_trans(Tween.TRANS_SINE)
	#tweenFade.tween_property(self, "modulate:a", 1.0, 1)
	tweenBob.play()


func _on_DialogueUi_BeginNextLine():
	#var tweenFade = get_tree().create_tween().bind_node(self).set_trans(Tween.TRANS_SINE)
	#tweenFade.tween_property(self, "modulate:a", 0.0, 1)
	tweenBob.stop()
	self.modulate.a = 0.0

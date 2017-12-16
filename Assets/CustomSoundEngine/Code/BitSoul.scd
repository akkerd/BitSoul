(
s.reboot;

~filePath = "C:/Users/ANIA/Documents/GitHub/BitSoul/Assets/CustomSoundEngine/Samples/";

~buffArrayIdle = [
	"hello.wav",
	"hey.wav",
	"look.wav",
	"listen.wav"
].collect({ arg snd; Buffer.read(Server.default, ~filePath++snd)});

// NOTE DEFS ///////////////////////////
~dur = PatternProxy(Pn(0.25, inf));
~repeat = PatternProxy(Pn(2, 4));

n = Pdef(\fmajor7, Pbind(\dur, ~dur, \freq, Pseq( [65,69,60,65,69,60,65,69,64].midicps)));
m = Pdef(\gmajor7, Pbind(\dur, ~dur, \freq, Pseq( [67,71,62,67,71,62,67,71,66].midicps)));

c = Pdef(\cmajor, Pbind(\dur, ~dur, \freq, Pseq( [60,64,67].midicps ,~repeat)));
f = Pdef(\fmajor, Pbind(\dur, ~dur, \freq, Pseq( [65,69,60].midicps ,~repeat)));
g = Pdef(\gmajor, Pbind(\dur, ~dur, \freq, Pseq( [67,71,62].midicps ,~repeat)));
e = Pdef(\eminor, Pbind(\dur, ~dur, \freq, Pseq( [64,67,71].midicps ,~repeat)));
a = Pdef(\aminor, Pbind(\dur, ~dur, \freq, Pseq( [69,60,64].midicps ,~repeat)));
d = Pdef(\dminor, Pbind(\dur, ~dur, \freq, Pseq( [62,65,69].midicps ,~repeat)));
b = Pdef(\bmajor, Pbind(\dur, ~dur, \freq, Pseq( [59,63,66].midicps ,~repeat)));

//SYNTH DEFS/////////////

//SoundFX Synth

SynthDef(\sampler, {
	arg out = 0, rate = 1, amp = 1, buffer = 0;
	var signal;

	signal = PlayBuf.ar(2, buffer, BufRateScale.kr(buffer)*rate, doneAction:2
	);

	Out.ar(out, signal * amp);
}).add;

SynthDef.new(\soundtrack, {
	arg ampHz=4, fund=40, maxPartial=4;
	var freq1, freq2, amp1, amp2, sig1, sig2;
	freq1 = LFNoise0.kr(4).exprange(fund, fund*maxPartial).round(fund);
	freq2 = LFNoise0.kr(4).exprange(fund, fund*maxPartial).round(fund);
	freq1 = freq1 * LFPulse.kr(8, add:1);
	freq1 = freq1 * LFPulse.kr(6, add:1);
	amp1 = LFPulse.kr(ampHz, 0, 0.12);
	amp2 = LFPulse.kr(ampHz, 0.5, 0.12);
	sig1 = Pulse.ar(freq1,0.5, amp1);
	sig2 = Pulse.ar(freq2, 0.5, amp2);
	sig1 = FreeVerb.ar(sig1, 0.7, 0.8, 0.25);
	sig2 = FreeVerb.ar(sig2, 0.7, 0.8, 0.25);
	Out.ar(0,sig1);
	Out.ar(1,sig2);
}).add;


OSCdef.new(
	\start,
	{arg msg;
		[msg].postln;

		x = Synth.new(\soundtrack);
	},
	'/start',
	nil, 57120
);

OSCdef.new(
	\player,
	{arg msg;
		[msg].postln;
		(
			// STOP all the previous sounds
			x.free;
			~x1.stop;
			~x2.stop;
			~y1.stop;
			~y2.stop;
			~z1.stop;
			~z2.stop;
			switch (msg.at(1).postln,
				1, { x = Synth.new(\soundtrack); },
				3, { ~dur.source = Pn(0.2,inf);  ~repeat.source = Pn(2, 4); ~z1 = Pseq([c,e,f,g,b,c,f,m], inf).play; ~z2 = Pseq([c,e,f,g,b,c,f,m], inf).play;},
				5, { ~dur.source = Pn(0.8, inf); ~repeat.source = Pn(2, 2); ~y1 = Pseq([d,a,c,g,d,c,a,m], inf).play; ~y2 = Pseq([d,a,c,g,d,c,a,m], inf).play;},
				7, {  ~dur.source = Pn(0.125,inf); ~repeat.source = Pn(2, 4); ~x1 = Pseq([a,c,g,f,a,c,g,n], 2).play; ~x2 = Pseq([a,c,g,f,a,c,g,n], inf).play;},
			);
		)
	},
	'/player',
	nil, 57120
);

OSCdef.new(
	\changeSpeedST,
	{arg msg;
		[msg].postln;
		x.set(\ampHz, msg);
	},
	'/changeSpeedST',
	nil, 57120
);

OSCdef.new(
	\stop,
	{
		x.free;
		~x1.stop;
		~x2.stop;
		~y1.stop;
		~y2.stop;
		~z1.stop;
		~z2.stop;
	},
	'/stop',
	nil, 57120
);

/*OSCdef.new(
	\sampler,
	{
		arg msg;
		[~buffArrayIdle].postln;
		Synth.new(\sampler, [\buffer, ~buffArrayIdle]);
	},
	'/sampler',
	nil, 57120
);*/

OSCdef.new(
	\sampler,
	{
		arg msg;
		[~filePath++msg.at(1)].postln;
		Synth.new(\sampler, [\buffer, Buffer.read(Server.default,~filePath++msg.at(1))]);
	},
	'/sampler',
	nil, 57120
);
)

x = Synth.new(\soundtrack);
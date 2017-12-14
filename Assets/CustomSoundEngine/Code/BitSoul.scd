(
s.reboot;

/*beat = [0,1,0,1];
loadTempo = TempoClock(1);
drumsTempo = TempoClock(0.5);
lvlCounter = 0;
newState = 0;
oldState = -1;
variSpeed = [1, 1.2, 1.4, 1.6, 1.8];*/

~filePath = "C:/Users/ANIA/Documents/GitHub/BitSoul/Assets/CustomSoundEngine/Samples/";

~buffArrayIdle = [
].collect({ arg snd; Buffer.read(Server.default, ~filePath+/+snd)});

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

SynthDef.new(\melody, {

}).add;


SynthDef(\music, {
	arg out = 0, rate = 1, amp = 0.5, buffer = 0, gate = 0;
	var signal, env;

	env = EnvGen.ar(Env.adsr(1, 1, 0.75, 1), gate);

	signal = PlayBuf.ar(1, buffer, BufRateScale.kr(buffer)*rate,
		loop:1
	);

	Out.ar(out, (signal*amp) * env);
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
	},
	'/stop',
	nil, 57120
);

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

//Amp Control
~sampleGroup = Group.head(s);
~ampGroup = Group.after(~sampleGroup);

~numTracks = 1;
~startTrack = 20;

~idleTrack = 20;

~idleSampler = Synth.head(~sampleGroup, \sampler, [\buffer,~buffArrayIdle, \out, ~idleTrack]);
//~chanAmps = ~numTracks.collect({arg i; Synth.tail(~ampGroup, \chanAmp, [\in, ~startTrack + [i*2], \out, 0, \amp, 1, \tag, i])});
)


Synth.new(\sampler, [\music, Buffer.read(Server.default,~filePath++"hello.wav"), \rate, 1]);
~filePath++"hello.wav".postln;

x = Synth.new(\soundtrack);
